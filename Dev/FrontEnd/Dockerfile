FROM node:alpine AS build
WORKDIR /front-end
COPY ./ ./
RUN npm ci
RUN npm run build

FROM node:alpine AS run
WORKDIR ./front-end
COPY --from=build /front-end/package.json ./
COPY --from=build /front-end/package-lock.json ./
COPY --from=build /front-end/.next ./

RUN npm ci
ENTRYPOINT [ "npm", "run", "dev" ] 
EXPOSE 80

# Dockerfile for frontend